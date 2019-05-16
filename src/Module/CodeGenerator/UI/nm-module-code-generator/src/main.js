import WebHost from 'nm-module-admin'
import config from './config'
import CodeGenerator from './index'

// 注入模块
WebHost.addModule(CodeGenerator)

// 启动
WebHost.start(config)
