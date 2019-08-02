import { loadRoutes } from 'nm-lib-utils'
const requireComponent = require.context('../views', true, /\page.js$/)
export default loadRoutes(
  requireComponent.keys().map(fileName => requireComponent(fileName).route)
)