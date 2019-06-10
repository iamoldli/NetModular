import WebHost from 'nm-module-admin'
import CodeGenerator from 'nm-module-code-generator'
import config from './config'

// 注入模块
WebHost.addModule(CodeGenerator)

// 启动
WebHost.start(config)
