import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    clear: root + 'Clear'
  }
  const clear = prefix => {
    return $http.delete(urls.clear, { prefix })
  }
  return {
    ...crud,
    clear
  }
}
