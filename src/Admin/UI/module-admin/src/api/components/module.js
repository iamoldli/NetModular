import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select',
    optionsEdit: root + 'OptionsEdit',
    optionsUpdate: root + 'OptionsUpdate'
  }

  /**
   * @description 编辑模块配置信息
   */
  const optionsEdit = code => {
    return $http.get(urls.optionsEdit, { code })
  }

  /**
   * @description 更新模块配置信息
   */
  const optionsUpdate = params => {
    return $http.post(urls.optionsUpdate, params)
  }

  /**
   * @description 下拉框
   */
  const select = () => {
    return $http.get(urls.select)
  }

  return { ...crud, select, optionsEdit, optionsUpdate }
}
