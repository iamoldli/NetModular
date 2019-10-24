import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    select: root + 'select',
    sync: root + 'sync'
  }

  /**
   * @description 下拉框
   */
  const select = () => {
    return $http.get(urls.select)
  }

  /**
   * @description 同步
   */
  const sync = () => {
    return $http.post(urls.sync)
  }

  return { ...crud, select, sync }
}
