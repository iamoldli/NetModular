import { http } from 'nm-lib-utils'
const root = 'Common/Area/'
const crud = http.crud(root)

const urls = {
  crawling: root + 'Crawling'
}

/**
 * @description 爬取数据
 */
const crawling = () => {
  return http.post(urls.crawling)
}

export default {
  ...crud,
  crawling
}
