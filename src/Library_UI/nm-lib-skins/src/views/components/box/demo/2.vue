<template>
  <nm-box header footer title="我是个有头有尾的盒子" icon="list">
    <nm-demo-desc>
      <p>
        盒子默认不包含头部和尾部，可通过
        <code>header</code>和
        <code>footer</code>开启
      </p>
      <p>
        头部标题可以通过
        <code>title</code>设置，可通过
        <code>icon</code>属性设置左侧的图标，同时也可以通过
        <code>header</code>插槽设置
      </p>
      <p>
        尾部只能通过
        <code>footer</code>插槽来设置内容
      </p>
    </nm-demo-desc>

    <template v-slot:footer>
      <nm-button type="success">确定</nm-button>
    </template>
  </nm-box>
</template>
