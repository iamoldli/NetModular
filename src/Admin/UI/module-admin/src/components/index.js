import library from 'netmodular-ui/packages/library'
let components = []
const requireComponent = require.context('./', true, /index\.vue$/)
requireComponent.keys().map(fileName => {
  components.push({
    name: `${library.prefix.toLowerCase()}-${fileName.split('/')[1]}`,
    component: requireComponent(fileName).default
  })
})
export default components
