import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)

  const urls = {
    tree: root + 'Tree',
    queryByCodes: root + 'QueryByCodes'
  }

  const tree = () => {
    return $http.get(urls.tree)
  }

  const queryByCodes = codes => {
    return $http.get(urls.queryByCodes, { codes })
  }

  return {
    ...crud,
    tree,
    queryByCodes
  }
}
