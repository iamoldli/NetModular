<template>
  <nm-drawer class="nm-skin-toggle" v-bind="deawer" :visible.sync="visible_" v-on="on">
    <el-divider>皮肤</el-divider>
    <section class="skin-list">
      <div v-for="item in list" :key="item.code" :class="['skin-list-item',model.name===item.code?'active':'']" @click="onSkinChange(item)">
        <div class="img">
          <img :src="item.preview" />
        </div>
        <div class="text">
          <span>{{item.name}}</span>&nbsp;&nbsp;
          <el-popover placement="left" :title="item.name" width="700" trigger="click">
            <img style="width:100%" :src="item.preview" />
            <nm-button slot="reference" type="text" text="产看大图" />
          </el-popover>
        </div>
      </div>
    </section>
    <el-divider>主题</el-divider>
    <section class="theme-list">
      <div
        :class="['theme-list-item',model.theme===item.name?'active':'']"
        v-for="item in themes"
        :key="item.name"
        :style="{backgroundColor:item.color}"
        @click="onThemeChange(item)"
      ></div>
    </section>
    <el-divider>字号</el-divider>
    <section class="fontsize-list">
      <el-radio v-model="model.fontSize" label="default" border>默认</el-radio>
      <el-radio v-model="model.fontSize" label="medium" border size="medium">中号</el-radio>
      <el-radio v-model="model.fontSize" label="small" border size="small">小号</el-radio>
      <el-radio v-model="model.fontSize" label="mini" border size="mini">超小</el-radio>
    </section>

    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="onSave" />
    </template>
  </nm-drawer>
</template>
<script>
import drawer from '../../../../mixins/components/drawer'
import { mapState, mapMutations } from 'vuex'
export default {
  mixins: [drawer],
  data() {
    return {
      deawer: {
        header: true,
        footer: true,
        title: '皮肤设置',
        icon: 'skin',
        width: '650px',
        fullscreen: false,
        modal: false
      },
      model: {
        name: '',
        theme: '',
        fontSize: ''
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  computed: {
    ...mapState('app/system', ['saveSkin']),
    ...mapState('app/skins', ['current', 'list']),
    currSkin() {
      return this.list.find(m => m.code === this.model.name)
    },
    themes() {
      return this.currSkin ? this.currSkin.themes : []
    }
  },
  methods: {
    ...mapMutations('app/skins', ['toggle']),
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.model = {
        name: this.current.name,
        theme: this.current.theme,
        fontSize: this.current.fontSize || 'default'
      }
    },
    onSkinChange(skin) {
      this.model.name = skin.code
      this.model.theme = skin.themes[0].name
      this.model.fontSize = skin.fontSizes[0].code
    },
    onThemeChange(theme) {
      this.model.theme = theme.name
    },
    onSave() {
      this.saveSkin(this.model).then(() => {
        window.location.reload()
      })
    }
  }
}
</script>
