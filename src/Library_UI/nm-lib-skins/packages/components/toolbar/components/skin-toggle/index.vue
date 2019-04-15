<template>
  <div class="nm-toolbar-item">
    <nm-button class="nm-toolbar-button" type="text" @click="visible=true">
      <nm-icon name="skin"/>
    </nm-button>
    <nm-dialog title="皮肤切换" icon="skin" btn-ok-text="切换" :btn-reset="false" :visible.sync="visible" :model="model" :action="action">
      <div class="skin-from">
        <div class="skin-subheading skin">skin layout</div>
        <skin-form-item :isActive="model.name=='pretty'" :skinName="'pretty'" class="skin-from-item" @isActiveSkin="isActiveSkin">
          <template slot>
            <skin-pretty :headerColor="selectTheme.headerColor" :sideColor="selectTheme.sideColor"></skin-pretty>
          </template>
        </skin-form-item>

        <!-- <skin-form-item :isActive="model.name=='classics'" :skinName="'classics'" class="skin-from-item" @isActiveSkin="isActiveSkin">
          <template slot>
            <skin-pretty></skin-pretty>
          </template>
        </skin-form-item>-->
      </div>
      <div class="skin-subheading header">header color</div>
      <div class="skin-from">
        <div
          class="theme-item-color"
          v-for="info in prettyList"
          :key="info.name"
          :class="selectTheme.headerColor==info.headerColor?'is-active':''"
          :style="'background:'+info.headerColor"
          @click="isActiveHeaderColor(info)"
        ></div>
      </div>
      <div class="skin-subheading side">side color</div>
      <div class="skin-from" style="padding-bottom: 25px;">
        <div
          class="theme-item-color"
          v-for="item in selectTheme.sideList"
          :key="item.theme"
          :class="selectTheme.themeName==item.theme?'is-active':''"
          :style="'background:'+item.sideColor"
          @click="isActiveSideColor(item)"
        >{{item.selected}}</div>
      </div>
      <template v-slot:footer>
        <nm-button type="primary" @click="checkTheme">切换</nm-button>
      </template>
    </nm-dialog>
  </div>
</template>
<script>

import skinPretty from './pretty'
import skinFormItem from './skin-from-item'
import themeList from './themeList'
import { mapMutations } from 'vuex'
export default {
  components: { skinPretty, skinFormItem },
  data () {
    return {
      /** 当前选择的主题 */
      selectTheme: { themeName: '', headerColor: '', sideColor: '', sideList: [] },
      visible: false,
      model: {
        /** 名称 */
        name: 'pretty',
        /** 主题 */
        theme: ''
        /** 字号 medium/small/mini */},
      action () { }
    }
  },
  created () {
    this.prettyList = themeList.prettyTheme
    this.selectTheme.sideList = this.prettyList[0].sideColor
    this.selectTheme.headerColor = this.prettyList[0].headerColor
    this.selectTheme.themeName = this.prettyList[0].sideColor[0].theme
    this.selectTheme.sideColor = this.prettyList[0].sideColor[0].sideColor
  },
  methods: {
    // 设置选中布局样式
    isActiveSkin (name) {
      this.model.name = name
    },
    // 设置选中头部颜色
    isActiveHeaderColor (info) {
      this.selectTheme.sideList = info.sideColor
      this.selectTheme.headerColor = info.headerColor
      this.selectTheme.themeName = info.sideColor[0].theme
      this.selectTheme.sideColor = info.sideColor[0].sideColor
      // this.themeChange(info.sideColor[0]);
    },
    // 设置选中侧边颜色
    isActiveSideColor (item) {
      this.selectTheme.themeName = item.theme
      this.selectTheme.sideColor = item.sideColor
      // this.themeChange(item);
    },
    // 切换主题及保存
    checkTheme () {
      this.model.theme = this.selectTheme.themeName
      this.themeChange(this.model)
    },
    ...mapMutations('app/skins', ['themeChange'])

  }

}
</script>
<style lang="scss">
.skin-from {
  position: relative;
  height: 100%;
  &::after,
  &::before {
    content: "";
    display: table;
    clear: both;
  }
}
.skin-subheading {
  color: #000;
  padding: 1.34rem 1rem;
  font-weight: 900;
  text-align: center;
  font-size: 1.25em;
  &::before,
  &::after {
    content: "";

    border-top: 1px rgba(0, 0, 0, 0.1) solid;
    display: inline-block;
    vertical-align: middle;
  }
  &.skin {
    &::before,
    &::after {
      content: "";
      width: calc(100% / 2 - 90px);
    }
  }
  &.side {
    &::before,
    &::after {
      content: "";
      width: calc(100% / 2 - 80px);
    }
  }
  &.header {
    &::before,
    &::after {
      content: "";
      width: calc(100% / 2 - 95px);
    }
  }
  &::before {
    margin-right: 30px;
  }
  &::after {
    margin-left: 30px;
  }
}
.theme-item-color {
  margin: 1px 1px 1px 30px;
  width: 2rem;
  height: 2rem;
  border-radius: 50%;
  transition: all 0.4s ease-in-out 0s;
  cursor: pointer;
  float: left;
  background: #dcdcdc;
  box-shadow: 0px 0px 10px 1px rgba(#b8b8b8d0, 1);
  &:hover {
    box-shadow: 0px 0px 10px 1px rgba(#b30eb9d0, 1);
  }
  &.is-active {
    box-shadow: 0px 0px 10px 1px rgba(#1e90ff, 1);
  }
}
.skin-from-item {
  float: left;
}
</style>
