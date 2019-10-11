import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    tree: root + 'Tree'
  }

  /**
   * 获取部门树
   * @param {公司编号} companyId
   */
  const getTree = companyId => {
    return $http.get(urls.tree, { companyId })
  }

  return {
    ...crud,
    getTree
  }
}
