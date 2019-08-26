import WebHost from './index'
import config from './config'

// 设置登录信息
// WebHost.setLoginSettings({
//   accountTypeOptions: [
//     { label: '管理员', value: 0 },
//     { label: '企业', value: 2 },
//     { label: '员工', value: 1 }
//   ]
// })

// 启动
WebHost.start(config)
