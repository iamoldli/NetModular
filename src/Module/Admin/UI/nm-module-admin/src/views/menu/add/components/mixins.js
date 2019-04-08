import api from '../../../../api/menu'
export default {
  data () {
    return {
      form: {
        noLoading: true,
        action: api.add,
        model: {
          // 所属模块
          moduleCode: '',
          // 父节点ID
          parentId: '',
          // 类型
          type: 0,
          // 名称
          name: '',
          // 路由名称
          routeName: '',
          // 路由参数
          routeParams: '',
          // 路由参数
          routeQuery: '',
          // 图标
          icon: '',
          // 图标颜色
          iconColor: '',
          // url链接
          url: '',
          // 是否显示
          show: true,
          // 链接打开目标
          target: 0,
          // 对话框宽度
          dialogWidth: '',
          // 对话框高度
          dialogHeight: '',
          // 对话框可全屏
          dialogFullscreen: true,
          // 排序
          sort: 0,
          // 备注
          remarks: ''
        },
        rules: {
          name: [{ required: true, message: '请输入菜单名称' }]
        }
      },
      on: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      }
    }
  },
  props: ['parent', 'sort'],
  methods: {
    submit () {
      this.$refs.form.submit()
    },
    reset () {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.setProps()
      })
    },
    setProps () {
      this.form.model.parentId = this.parent.id
      this.form.model.sort = this.sort
    },
    onSuccess () {
      this.$emit('success')
    },
    onError () {
      this.$emit('error')
    },
    onValidateError () {
      this.$emit('validate-error')
    }
  }
}
