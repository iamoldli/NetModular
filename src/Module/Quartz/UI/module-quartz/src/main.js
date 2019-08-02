import WebHost from 'nm-module-admin'
import config from './config'
import Quartz from './index'

// 注入模块
WebHost.addModule(Quartz)

// 启动
WebHost.start(config)
