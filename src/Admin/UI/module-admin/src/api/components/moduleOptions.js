import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const urls = {
    edit: root + 'edit',
    update: root + 'update'
  }

  /**
   * @description 编辑模块配置信息
   */
  const edit = moduleCode => {
    return $http.get(urls.edit, { moduleCode })
  }

  /**
   * @description 更新
   */
  const update = params => {
    return $http.post(urls.update, params)
  }

  return { edit, update }
}
