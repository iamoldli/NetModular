import { http } from 'nm-lib-utils'
const root = 'admin/permission/'
const crud = http.crud(root)

const urls = {
  controllers: root + 'Controllers',
  actions: root + 'Actions'
}

/**
 * @description 获取控制器列表
 */
export const controllers = moduleCode => {
  return http.get(urls.controllers, { moduleCode })
}

/**
 * @description 获取控制器的方法列表
 */
export const actions = params => {
  return http.get(urls.actions, params)
}

export default {
  ...crud,
  controllers,
  actions
}
