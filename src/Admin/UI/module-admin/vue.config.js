const path = require('path')
const CopyWebpackPlugin = require('copy-webpack-plugin')
const TerserPlugin = require('terser-webpack-plugin')
// 开发环境
const isDev = process.env.NODE_ENV === 'development'
// 打包输出路径
const outputDir = '../../WebHost/wwwroot/app'

// 环境变量
/**版权信息 */
process.env.VUE_APP_COPYRIGHT = '版权所有：尼古拉斯·老李 | 用代码改变世界'
/**版本号 */
process.env.VUE_APP_BUILD_TIME = require('dayjs')().format('YYYYMDHHmmss')
/**第三方依赖组件，写法示例：<script src="."></script> */
process.env.VUE_APP_CUSTOM_SCRIPTS = ''

module.exports = {
  outputDir: outputDir,
  publicPath: '/app',
  devServer: {
    port: 5220
  },
  transpileDependencies: ['netmodular-*', 'element-ui'],
  configureWebpack() {
    let config = {
      plugins: [
        /**
         * 复制netmodular-ui/public目录下的文件到输出目录
         */
        new CopyWebpackPlugin([
          {
            from: path.join(__dirname, 'node_modules/netmodular-ui/public'),
            to: path.join(__dirname, outputDir),
            ignore: ['index.html']
          }
        ])
      ]
    }

    if (!isDev) {
      //自定义代码压缩
      config.optimization = {
        minimize: true,
        minimizer: [
          new TerserPlugin({
            cache: true,
            parallel: true,
            sourceMap: false,
            terserOptions: {
              compress: {
                drop_console: true,
                drop_debugger: true
              }
            }
          })
        ]
      }
    }
    return config
  },
  chainWebpack: config => {
    /**
     * 删除懒加载模块的 prefetch preload，降低带宽压力
     * https://cli.vuejs.org/zh/guide/html-and-static-assets.html#prefetch
     * https://cli.vuejs.org/zh/guide/html-and-static-assets.html#preload
     * 而且预渲染时生成的 prefetch 标签是 modern 版本的，低版本浏览器是不需要的
     */
    config.plugins.delete('prefetch').delete('preload')

    /**
     * 设置index.html模板路径，使用netmodular-ui/public中的模板
     */
    config.plugin('html').tap(args => {
      args[0].template = './node_modules/netmodular-ui/public/index.html'
      return args
    })

    config
      // 开发环境
      .when(
        isDev,
        // sourcemap不包含列信息
        config => config.devtool('cheap-source-map')
      )
      // 非开发环境
      .when(!isDev, config => {
        // 拆分
        config.optimization.splitChunks({
          chunks: 'all',
          cacheGroups: {
            elementUI: {
              name: 'chunk-element-ui',
              priority: 20,
              test: /[\\/]node_modules[\\/]element-ui(.*)/
            },
            skins: {
              name: 'chunk-netmodular-ui',
              priority: 10,
              test: /[\\/]node_modules[\\/]netmodular-ui(.*)/
            }
          }
        })

        config.optimization.runtimeChunk({
          name: 'manifest'
        })
      })
  }
}
