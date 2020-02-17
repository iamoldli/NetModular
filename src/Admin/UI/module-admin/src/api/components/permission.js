import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    controllers: root + 'Controllers',
    actions: root + 'Actions',
    sync: root + 'sync',
    tree: root + 'Tree'
  }

  /**
   * @description 同步
   */
  const sync = () => {
    return $http.post(urls.sync)
  }

  const tree = () => {
    return $http.get(urls.tree)
  }
  return {
    ...crud,
    sync,
    tree
  }
}
