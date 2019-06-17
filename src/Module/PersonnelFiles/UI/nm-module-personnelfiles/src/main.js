import WebHost from 'nm-module-admin'
import Common from 'nm-module-common'
import config from './config'
import PersonnelFiles from './index'

// 注入模块
WebHost.addModule(PersonnelFiles)

// 注入通用模块
WebHost.addModule(Common)

// 启动
WebHost.start(config)
