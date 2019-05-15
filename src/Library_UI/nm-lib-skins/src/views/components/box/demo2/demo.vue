<template>
  <nm-box header footer title="头尾示例" icon="app">
    <el-alert title="头部设置" description="title、icon属性必须在header属性为true的时候才会生效" type="warning" :closable="false" show-icon></el-alert>
    <el-alert title="底部设置" description="footer插槽必须在footer属性为true的时候才会生效" type="warning" :closable="false" show-icon></el-alert>
    <template v-slot:footer>
      <nm-button type="success" text="确认"/>
    </template>
  </nm-box>
</template>
