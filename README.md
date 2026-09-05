# AI 对话集成 Demo（semantic-kernel-ollama-demo）

![lang](https://img.shields.io/badge/lang-C#-informational) ![status](https://img.shields.io/badge/status-maintained-brightgreen)


使用 **Microsoft Semantic Kernel** 接入本地 **Ollama** 的 .NET 对话示例，验证本地大模型集成链路。

##  功能特性

-  **本地 LLM 对话**：通过 Semantic Kernel 调用 Ollama（qwen2.5-coder:7b）
-  **标准接入流程**：Kernel 构建  Ollama ChatCompletion 配置  对话调用
-  **聊天示例**：完整的控制台对话演示

##  技术栈

| 组件 | 技术 |
|---|---|
| 语言 | C#（.NET） |
| AI 框架 | Microsoft Semantic Kernel |
| 本地模型 | Ollama + qwen2.5-coder:7b |
| 项目 | console app（SKOllamaDemo） |

##  快速开始

```bash
# 1. 安装并启动 Ollama，拉取模型
ollama pull qwen2.5-coder:7b

# 2. 运行项目
dotnet run
```

> 默认连接 `http://localhost:11434`（Ollama 默认端口），模型 `qwen2.5-coder:7b`。

##  项目结构

```
├── Program.cs        # 主程序：Kernel 构建 + Ollama 对话
├── SKOllamaDemo.csproj
└── AI_TRIAL.sln
```

##  用途

- 验证 .NET 侧本地大模型集成链路
- 可作为 AI 应用 / 后端方向的参考项目（LLM 接入模式）
