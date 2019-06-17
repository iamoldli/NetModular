import WebHost from 'nm-module-admin'
import config from './config'
import Common from './index'

// 注入模块
WebHost.addModule(Common)

// 启动
WebHost.start(config)
