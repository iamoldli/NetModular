<template>
  <nm-box page>
    <el-row>
      <p>删除按钮包含了普通按钮的所有属性，同时封装了二次提示以及执行api后的信息提示功能，删除成功后会触发success事件，可以通过监听该事件在删除成功后执行一些操作</p>
      <br>
      <p>删除按钮需要指定删除逻辑方法action以及删除数据的id(id属性可以接受任意类型的数据Number, String, Object, Array)，同时也可以通过msg属性指定删除成功后的提示信息</p>
      <br>
      <p>可通过no-icon属性设置不显示图标</p>
      <br>
      <nm-button-delete type="danger" :action="remove" id="1" @success="onSuccess"/>
    </el-row>

    <el-row style="height:400px">
      <p>批量删除按钮与上面的删除按钮用法基本一致，不同之处在于它是与表格结合使用的，需要设置selection属性，用于选择要删除的数据</p>
      <br>
      <p>可以通过show-property属性，设置二次提示时展示要删除的数据，show-property是要展示的数据在table中对应的列名称</p>
      <br>
      <nm-list v-bind="list">
        <template v-slot:toolbar="{selection}">
          <nm-button-delete-batch type="danger" :action="remove" :selection="selection" show-property="name" @success="onSuccess"/>
        </template>
      </nm-list>
    </el-row>
    <!--在线编辑预览不支持闭合的方式，请使用下面的代码方式-->
    <!-- <nm-button>默认按钮</nm-button> -->
  </nm-box>
</template>
<script>
export default {
  data () {
    return {
      list: {
        title: '批量删除示例',
        action: this.query,
        multiple: true,
        search: { enabled: false },
        cols: [{
          name: 'id',
          label: '编号',
          width: 240
        },
        {
          name: 'name',
          label: '名称'
        }]
      }
    }
  },
  methods: {
    remove () {
      return new Promise(resolve => {
        resolve({ code: 1 })
      })
    },
    query () {
      return new Promise(resolve => {
        resolve({ rows: [{ id: 1, name: '张三' }, { id: 2, name: '李四' }], total: 2 })
      })
    },
    onSuccess () {
      this._success('删除成功了')
    }
  }
}
</script>

<style>
.el-row {
  margin-bottom: 20px;
}
.el-button-group {
  margin-right: 20px;
}
</style>
