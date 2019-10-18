import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select'
  }

  /**
   * @description 查询字典下拉列表
   * @param {Object} params 参数
   */
  const select = params => {
    return $http.get(urls.select, params)
  }

  return {
    ...crud,
    select
  }
}
