import WebHost from 'nm-module-admin'
import CodeGenerator from 'nm-module-code-generator'
import config from './config'
import Blog from './index'

// 注入模块
WebHost.addModule(Blog)

// 注入模块
WebHost.addModule(CodeGenerator)

// 启动
WebHost.start(config)
