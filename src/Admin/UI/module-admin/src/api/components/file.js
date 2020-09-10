import module from '../../module'

export default name => {
  const root = `${module.code}/${name}/`
  const crud = $http.crud(root)
  const urls = {
    upload: root + 'Upload',
    hardDelete: root + 'HardDelete'
  }

  const getUploadUrl = () => {
    return `${$http.axios.defaults.baseURL}${urls.upload}`
  }

  const hardDelete = id => {
    return $http.delete(urls.hardDelete, { id })
  }

  const getByFullPath = fullPath => {
    return $http.get(root + fullPath)
  }

  // 图片预览
  const preview = url => {
    console.log(url)
    return $http.preview(url)
  }

  // 文件下载
  const download = url => {
    return $http.download(url)
  }

  return {
    ...crud,
    getUploadUrl,
    hardDelete,
    getByFullPath,
    preview,
    download
  }
}
