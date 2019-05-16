import { mixins } from 'nm-lib-skins'
const typeArr = [
  'String',
  'Byte',
  'Short',
  'Int',
  'Long',
  'Double',
  'Decimal',
  'Bool',
  'Guid',
  'DateTime',
  'Enum'
]
let i = 0
const options = typeArr.map(item => {
  return { label: item, value: i++ }
})
export default {
  mixins: [mixins.select],
  data() {
    return {
      action() {
        return new Promise(resolve => {
          resolve(options)
        })
      }
    }
  }
}
