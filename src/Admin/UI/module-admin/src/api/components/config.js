import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const urls = {
    ui: root + 'UI',
    edit: root + 'Edit',
    update: root + 'Update',
    descriptors: root + 'Descriptors'
  }

  const getUI = () => {
    return $http.get(urls.ui)
  }

  const edit = params => {
    return $http.get(urls.edit, params)
  }

  const update = params => {
    return $http.post(urls.update, params)
  }

  const getDescriptors = () => {
    return $http.get(urls.descriptors)
  }

  return {
    getUI,
    edit,
    update,
    getDescriptors
  }
}
