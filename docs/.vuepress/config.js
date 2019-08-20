module.exports = {
  base:'/docs/',
  port: 7220,
  dest: '../src/WebHost/wwwroot/docs',
  title: 'NetModular',
  head: [['link', { rel: 'icon', href: '/images/logo.png' }]],
  description: '基于.Net Core的模块化快速开发框架',
  themeConfig: {
    nav: [
      { text: '首页', link: '/' },
      { text: '指南', link: '/guide/' },
      { text: 'Demo', link: 'http://nm.iamoldli.com' },
      {
        text: '选择语言',
        items: [
          { text: '简体中文', link: '/' },
          { text: 'English', link: '/en' }
        ]
      },
      { text: 'GitHub', link: 'https://github.com/iamoldli/NetModular' }
    ]
  }
}
