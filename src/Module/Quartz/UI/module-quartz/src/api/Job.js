import { http } from 'nm-lib-utils'
const root = 'Quartz/Job/'
const crud = http.crud(root)
const urls = {
  moduleSelect: root + 'ModuleSelect',
  jobSelect: root + 'JobSelect',
  pause: root + 'Pause',
  resume: root + 'Resume',
  log: root + 'log'
}

const moduleSelect = () => {
  return http.get(urls.moduleSelect)
}

const jobSelect = moduleId => {
  return http.get(urls.jobSelect, { moduleId })
}

const pause = id => {
  return http.post(urls.pause + '?id=' + id)
}

const resume = id => {
  return http.post(urls.resume + '?id=' + id)
}
const log = params => {
  return http.get(urls.log, params)
}
export default {
  ...crud,
  moduleSelect,
  jobSelect,
  pause,
  resume,
  log
}
