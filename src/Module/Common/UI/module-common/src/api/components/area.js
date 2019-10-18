import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    queryChildren: root + 'QueryChildren'
  }

  /**
   * @description 查询子节点
   */
  const queryChildren = parentCode => {
    return $http.get(urls.queryChildren, { parentCode })
  }

  return {
    ...crud,
    queryChildren
  }
}
