#pragma warning disable SKEXP0070 
using Microsoft.SemanticKernel; // Semantic Kernel 的核心
using Microsoft.SemanticKernel.ChatCompletion; // 用于聊天功能
using Microsoft.SemanticKernel.Connectors.Ollama; // 用于连接 Ollama

// 1. 构建 Kernel 对象
//    Kernel 是 Semantic Kernel 的核心，负责协调所有 AI 操作
var builder = Kernel.CreateBuilder();

// 2. 配置 Ollama 聊天服务
//    告诉 Kernel 使用哪个模型和 Ollama 的地址
#pragma warning disable CS0618 // 类型或成员已过时
builder.AddOllamaChatCompletion(
    modelId: "qwen2.5-coder:7b",        // 模型名称，必须与 Ollama 中的一致
    endpoint: new Uri("http://localhost:11434") // Ollama 服务地址
);
#pragma warning restore CS0618 // 类型或成员已过时

// 3. 构建 Kernel 实例
var kernel = builder.Build();

// 4. 获取聊天服务
//    从 Kernel 中获取我们刚刚配置好的聊天服务对象
var chatService = kernel.GetRequiredService<IChatCompletionService>();

// 5. 创建聊天历史记录
//    ChatHistory 对象会自动管理对话的上下文
var history = new ChatHistory();

// 可选：给 AI 一个初始的“人设”或系统指令
history.AddSystemMessage("你是一位经验丰富的 C# 编程专家，回答需要简洁且提供代码示例。");

// 6. 开启一个简单的命令行聊天循环
Console.WriteLine($"正在和 qwen2.5-coder:7b 对话，输入 'exit' 可退出程序。");
Console.WriteLine("------------------------------------------------------");

while (true)
{
    // 获取用户输入
    Console.Write("\n问: ");
    var userMessage = Console.ReadLine();

    if (string.IsNullOrEmpty(userMessage) || userMessage.ToLower() == "exit")
    {
        break;
    }

    // 将用户的问题添加到聊天历史中
    history.AddUserMessage(userMessage);

    // 获取 AI 的流式回复
    // GetStreamingChatMessageContentsAsync 方法会返回一个异步流，实现流式输出
    Console.Write("答: ");
    string fullResponse = "";
    await foreach (var chunk in chatService.GetStreamingChatMessageContentsAsync(history))
    {
        // 如果 chunk 有内容，就输出到控制台
        if (chunk.Content != null)
        {
            Console.Write(chunk.Content);
            fullResponse += chunk.Content;
        }
    }

    // 将 AI 的完整回复也添加到聊天历史中，这样它就能记住我们聊过什么
    history.AddAssistantMessage(fullResponse);
    Console.WriteLine(); // 输出一个换行
}