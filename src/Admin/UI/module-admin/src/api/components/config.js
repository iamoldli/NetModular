import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const urls = {
    ui: root + 'UI',
    edit: root + 'Edit',
    update: root + 'Update',
    uploadLogo: root + 'UploadLogo',
    logoUrl: root + 'LogoUrl',
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

  /**
   * logo上传接口
   */

  const uploadLogoUrl = () => {
    return $http.axios.defaults.baseURL + urls.uploadLogo
  }

  const getLogoUrl = () => {
    return $http.get(urls.logoUrl)
  }

  const getDescriptors = () => {
    return $http.get(urls.descriptors)
  }

  return {
    getUI,
    edit,
    update,
    uploadLogoUrl,
    getLogoUrl,
    getDescriptors
  }
}
