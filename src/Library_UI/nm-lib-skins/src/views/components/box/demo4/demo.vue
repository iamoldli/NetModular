<template>
  <nm-box v-bind="box">
    <el-alert title="盒子页" type="warning" description="通过设置 page 属性为true，可以让盒子变成一个盒子页，盒子页高度会自动填充满整个父级元素，当内容超出时会有滚动条" show-icon :closable="false"></el-alert>
    <pre>
    水调歌头·明月几时有

         苏轼

    明月几时有？把酒问青天。

    不知天上宫阙，今夕是何年。

    我欲乘风归去，又恐琼楼玉宇，高处不胜寒。

    起舞弄清影，何似在人间？

    转朱阁，低绮户，照无眠。

    不应有恨，何事长向别时圆？

    人有悲欢离合，月有阴晴圆缺，此事古难全。

    但愿人长久，千里共婵娟
    </pre>

    <template v-slot:footer>
      <nm-button type="success" text="确认" @click="onClick"/>
    </template>
  </nm-box>
</template>
<script>
export default {
  data() {
    return {
      box: {
        page: true,
        header: true,
        footer: true,
        title: '盒子页示例',
        icon: 'app',
        fullscreen: true,
        collapse: true
      }
    }
  },
  methods: {
    onClick() {
      this._success('保存成功')
    }
  }
}
</script>
