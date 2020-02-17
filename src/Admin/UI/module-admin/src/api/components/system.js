import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    config: root + 'config',
    updateBaseConfig: root + 'updateBaseConfig',
    updateComponentConfig: root + 'updateComponentConfig',
    updateLoginConfig: root + 'updateLoginConfig',
    updatePermissionConfig: root + 'updatePermissionConfig',
    updateToolbarConfig: root + 'updateToolbarConfig',
    updatePathConfig: root + 'updatePathConfig',
    uploadLogo: root + 'UploadLogo',
    allController: root + 'AllController',
    allAction: root + 'AllAction'
  }

  /**
   * @description 获取系统配置
   */
  const getConfig = () => {
    return $http.get(urls.config)
  }

  /**
   * @description 修改基础配置
   */
  const updateBaseConfig = params => {
    return $http.post(urls.updateBaseConfig, params)
  }

  /**
   * @description 修改组件配置
   */
  const updateComponentConfig = params => {
    return $http.post(urls.updateComponentConfig, params)
  }

  /**
   * @description 修改登录配置
   */
  const updateLoginConfig = params => {
    return $http.post(urls.updateLoginConfig, params)
  }

  /**
   * @description 修改权限配置
   */
  const updatePermissionConfig = params => {
    return $http.post(urls.updatePermissionConfig, params)
  }

  /**
   * @description 修改工具栏配置
   */
  const updateToolbarConfig = params => {
    return $http.post(urls.updateToolbarConfig, params)
  }

  /**
   * @description 修改路径配置
   */
  const updatePathConfig = params => {
    return $http.post(urls.updatePathConfig, params)
  }

  /**
   * logo上传接口
   */

  const uploadLogoUrl = () => {
    return $http.axios.defaults.baseURL + urls.uploadLogo
  }

  /** 获取指定模块的所有Controller的下拉列表 */
  const getAllController = params => {
    return $http.get(urls.allController, params)
  }

  /** 获取指定Controller的下拉列表 */
  const getAllAction = params => {
    return $http.get(urls.allAction, params)
  }

  return {
    ...crud,
    getConfig,
    updateBaseConfig,
    updateComponentConfig,
    updateLoginConfig,
    updatePermissionConfig,
    updateToolbarConfig,
    updatePathConfig,
    uploadLogoUrl,
    getAllController,
    getAllAction
  }
}
