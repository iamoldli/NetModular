// 消息提示
const duration = 2500
export default {
  methods: {
    /**
     * @description 成功消息
     * @param {String} msg 消息内容
     */
    async _success (msg, onClose) {
      await this.$message({
        message: msg,
        showClose: true,
        type: 'success',
        duration,
        onClose
      })
    },
    /**
     * @description 警告消息
     * @param {String} msg 消息内容
     */
    async _warning (msg, onClose) {
      await this.$message({
        message: msg,
        showClose: true,
        type: 'warning',
        duration,
        onClose
      })
    },
    /**
     * @description 错误消息
     * @param {String} msg 消息内容
     */
    async _error (msg, onClose) {
      await this.$message({
        message: msg,
        showClose: true,
        type: 'error',
        duration,
        onClose
      })
    },
    /** 二次确认 */
    async _confirm (msg, title, type) {
      await this.$confirm(msg, title || '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: type || 'warning'
      })
    },
    /** 删除数据确认 */
    async _delete (action, title) {
      const msg = title || '您确认要删除该数据吗?'

      await this._confirm(msg)

      // 执行删除操作
      if (action || typeof action === 'function') {
        await action()
        this._success('删除成功~')
      }
    }
  }
}
