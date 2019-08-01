<template>
  <nm-container>
    <nm-box v-bind="box">
      <template v-slot:toolbar>
        <el-color-picker v-model="color" size="small" style="position: absolute;top:3px;left:-40px" />
        <el-input class="nm-icon-picker-panel-filter" v-model="filter" placeholder="请输入英文或中文名称" clearable></el-input>
      </template>
      <div v-for="icon in iconList" :key="icon.code" class="nm-admin-icon-item">
        <div class="icon">
          <nm-icon :name="icon.code" :style="{color}" />
        </div>
        <span class="text">{{icon.name}}</span>
        <span class="code">{{icon.code}}</span>
      </div>
    </nm-box>
  </nm-container>
</template>
<script>
import page from './page'
import { iconData } from 'nm-lib-icon'

export default {
  name: page.name,
  data() {
    return {
      color: '#333',
      filter: '',
      box: {
        header: true,
        title: '图标',
        icon: 'icon',
        page: true,
        fullscreen: true
      }
    }
  },
  computed: {
    iconList() {
      if (!this.filter) { return iconData }

      let list = []
      iconData.forEach(icon => {
        if (icon.name.indexOf(this.filter) > -1 || icon.code.indexOf(this.filter) > -1) { list.push(icon) }
      })
      return list
    }
  }
}
</script>
<style lang="scss">
.nm-admin-icon-item {
  margin: 5px;
  padding: 5px;
  text-align: center;
  width: 80px;
  height: 80px;
  border: 1px solid #ccc;
  float: left;
  .icon {
    margin: 3px 0 10px 0;
    font-size: 2.5em;
    cursor: pointer;
    transition: all 0.25s ease-in;

    &:hover {
      transform: scale(1.2);
      color: #f39c12;
    }
  }

  .text,
  .code {
    display: block;
    font-size: 12px;
  }
  .text {
    margin-bottom: 3px;
  }
}
</style>
