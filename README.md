# KupaKuper HMI 项目

## 项目简介

KupaKuper HMI 是一个基于 WinForms 开发的人机界面系统，专为工业设备控制和监控设计。该系统提供了直观的操作界面，支持设备配置、报警监控、数据统计等功能。

## 主要功能

### 1. 设备配置管理
- 通过 Excel 模板快速配置设备参数
- 支持 JSON 配置文件的导入导出
- 多语言支持（中文、英文、越南语）

### 2. 监控功能
- 实时报警监控和统计
- 生产数据记录和分析
- 设备状态可视化

### 3. 数据管理
- 生产数据采集和存储
- 历史数据查询和分析
- 数据导出功能

### 4. 系统特点
- 模块化设计，易于扩展
- 直观的用户界面
- 支持多设备类型
- 完善的错误处理机制

## 项目结构

```
KupaKuper_HMI/
├── KupaKuper_HMI/           # 主应用程序
│   ├── DeviceMode/          # 设备配置
│   ├── Test/                # 测试数据
│   ├── Language/            # 语言文件
│   └── ConfigTemplate.xlsx  # 配置模板
├── KupaKuper_HMI_ConfigTool/ # 配置工具
│   ├── Help/                # 辅助类
│   └── Form1.cs             # 主界面
├── WinFromFrame_KupaKuper/   # WinForm 框架
│   ├── Help/                # 辅助工具
│   ├── Models/              # 数据模型
│   ├── ViewModels/          # 视图模型
│   └── Pages/               # 功能页面
└── RapidDevelopment_KupaKuper/ # 快速开发工具
```

## 快速开始

### 1. 配置设备
1. 运行 `KupaKuper_HMI_ConfigTool` 工具
2. 点击「新建配置表」创建新的设备配置
3. 填写设备参数并保存
4. 将生成的配置文件复制到主应用程序的 `DeviceMode` 目录

### 2. 运行主应用
1. 运行 `KupaKuper_HMI` 主应用程序
2. 登录系统（默认账号密码可在配置文件中设置）
3. 在主界面查看设备状态和监控数据

### 3. 查看报警和数据
- 切换到「报警」页面查看实时报警
- 切换到「数据」页面查看生产数据统计

## 技术栈

- C# WinForms
- Excel 操作（NPOI）
- JSON 序列化/反序列化
- 多语言支持
- 数据可视化

## 系统要求

- Windows 7 及以上操作系统
- .NET Framework 4.7.2 及以上
- Microsoft Excel 2007 及以上（用于配置文件编辑）

## 注意事项

- 配置文件修改后需重启应用程序生效
- 生产数据会自动保存到 `Test/ProductData` 目录
- 多语言切换可在系统设置中进行

## 许可证

本项目为内部使用系统，未经授权不得用于商业用途。

---

© 2026 KupaKuper HMI 项目组