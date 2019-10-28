import loadRoutes from 'netmodular-ui/packages/utils/load-routes'
const requireComponent = require.context('../views', true, /\page.js$/)
export default loadRoutes(
  requireComponent.keys().map(fileName => requireComponent(fileName).route)
)
