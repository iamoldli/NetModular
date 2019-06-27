<template>
  <nm-drawer v-bind="drawer" :visible.sync="visible_" @open="onOpen">
    <nm-details ref="details" v-bind="details"></nm-details>
    <nm-details ref="contactDetails" v-bind="contactDetails">
      <template v-slot:content-provinceName="{model}">{{model.provinceName||''}} {{model.cityName||''}} {{model.areaName||''}} {{model.townName||''}}</template>
    </nm-details>
  </nm-drawer>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../../api/User'
export default {
  mixins: [mixins.drawer],
  data() {
    return {
      drawer: {
        header: true,
        title: '用户详情',
        icon: 'detail',
        width: '50%',
        noPadding: true,
        fullscreen: true
      },
      details: {
        model: this.user,
        labelWidth: '130px',
        queryOnCreated: false,
        options: [
          [{ label: '工号', prop: 'jobNo' }, { label: '姓名', prop: 'name' }],
          [{ label: '出生日期', prop: 'birthday' }, { label: '性别', prop: 'sex' }],
          [{ label: '部门', prop: 'departmentName' }, { label: '岗位', prop: 'positionName' }],
          [{ label: '籍贯', prop: 'native' }, { label: '民族', prop: 'nation' }],
          [{ label: '手机', prop: 'phone' }, { label: '邮箱', prop: 'email' }],
          [{ label: '身份证号', prop: 'idCardNo' }]
        ]
      },
      contactDetails: {
        action: this.queryContactDetails,
        labelWidth: '130px',
        queryOnCreated: false,
        options: [
          [{ label: '居住地址', prop: 'provinceName' }],
          [{ label: '详细地址', prop: 'address' }],
          [{ label: 'QQ', prop: 'qq' }, { label: '微信', prop: 'wechat' }],
          [{ label: '钉钉', prop: 'dingDing' }]
        ]
      }
    }
  },
  props: {
    user: {
      type: Object,
      required: true
    }
  },
  methods: {
    queryContactDetails() {
      return api.contactDetails(this.user.id)
    },
    onOpen() {
      this.details.model = this.user
      this.$refs.contactDetails.refresh()
    }
  }
}
</script>
