对[axios](https://github.com/axios/axios)进行的封装。
包含`get`、`post`、`delete`、`put`四个方法

## 使用方法，以企业信息为例

### 1、在前端项目的 api 目录中添加 enterprise.js 文件

### 2、导入 http 模块

```js
import { http } from 'nm-lib-utils'
```

### 3、定义接口的基础路径

```js
const root = 'archive/enterprise/'
```

### 4、创建基础 CRUD 对象

```js
const crud = http.crud(root)
```

### 5、定义其他接口 url

```js
const urls = {
  select: root + 'select'
}
```

### 6、添加其他方法

```js
/**
 * @description 下拉框
 */
const select = () => {
  return http.get(urls.select)
}
```

### 7、导出所有方法

```js
export default { ...crud, select }
```

### 8、在页面中导入

```js
import api from '../../../api/enterprise'
```

通过 `api.query` 等方式调用

一个接口文件的完整实例如下

```js
import { http } from 'nm-lib-utils'
const root = 'admin/permission/'
const crud = http.crud(root)

const urls = {
  batchAdd: root + 'batchadd',
  actionTypeSelect: root + 'actionTypeSelect'
}

/**
 * @description 批量添加
 */
export const batchAdd = params => {
  return http.post(urls.batchAdd, params)
}

/**
 * @description 获取操作类型下拉列表
 */
export const actionTypeSelect = () => {
  return http.get(urls.actionTypeSelect)
}

export default { ...crud, batchAdd, actionTypeSelect }
```
