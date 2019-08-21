# 基础平台说明

::: tip
由于采用模块化的设计理念，模块之间都是独立的，所以每个模块需要独立的仓储进行维护，那么为了更好的管理代码，我们需要搭建私有的 Git 服务器，推荐[Gogs](https://gogs.io/)。
:::

::: tip
每个模块都包含至少四个项目，对应的都有四个 NuGet 包，为了更好的管理模块之间的依赖关系，所以需要搭建私有的 NuGet 服务器。推荐[BaGet](https://loic-sharma.github.io/BaGet/)。
:::

::: tip
为了更好的管理模块的前端代码，采用 NPM 包的形式，所以需要搭建私有的 NPM 服务器。推荐[verdaccio](https://verdaccio.org/docs/zh-CN/installation)。
:::
