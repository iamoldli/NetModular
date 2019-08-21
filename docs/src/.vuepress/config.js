module.exports = {
  base: '/docs/',
  port: 7220,
  dest: '../src/WebHost/wwwroot/docs',
  head: [['link', { rel: 'icon', href: '/images/logo.png' }]],
  markdown: {
    lineNumbers: true
  },
  configureWebpack: {
    resolve: {
      alias: {
        '@img': 'images'
      }
    }
  },
  locales: {
    '/': {
      lang: 'zh-CN',
      title: 'NetModular',
      description: '为中小型企业而生的基于.Net Core平台的模块化快速开发解决方案'
    }
  },
  themeConfig: {
    locales: {
      '/': {
        nav: [
          { text: '首页', link: '/' },
          { text: '指南', link: '/guide/' },
          {
            text: '选择语言',
            items: [
              { text: '简体中文', link: '/' },
              { text: 'English', link: '/en/' }
            ]
          },
          { text: 'GitHub', link: 'https://github.com/iamoldli/NetModular' }
        ],
        sidebar: [
          {
            title: '指南',
            collapsable: false,
            children: [
              ['/guide/', '介绍'],
              ['/guide/GetingStart', '快速上手'],
              ['/guide/CreateModule', '创建一个模块']
            ]
          },
          {
            title: '基础平台',
            collapsable: false,
            children: [
              ['/platform/', '说明'],
              ['/platform/Git', '搭建Git服务器'],
              ['/platform/NuGet', '搭建Nuget服务器'],
              ['/platform/Npm', '搭建Npm服务器']
            ]
          },
          {
            title: '扩展技能',
            collapsable: false,
            children: [['/extend/MSBuild', 'MSBuild']]
          }
        ]
      }
    }
  }
}
