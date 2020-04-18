import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select',
    sync: root + 'Sync'
  }

  /**
   * @description 更新模块配置信息
   */
  const sync = () => {
    return $http.post(urls.sync)
  }

  /**
   * @description 下拉框
   */
  const select = () => {
    return $http.get(urls.select)
  }

  return { ...crud, select, sync }
}
