<template>
  <nm-box page>
    <el-row>
      <nm-button text="默认按钮"></nm-button>
      <nm-button type="primary" text="主要按钮"></nm-button>
      <nm-button type="success" text="成功按钮"></nm-button>
      <nm-button type="info" text="信息按钮"></nm-button>
      <nm-button type="warning" text="警告按钮"></nm-button>
      <nm-button type="danger" text="危险按钮"></nm-button>
    </el-row>

    <el-row>
      <nm-button plain text="朴素按钮"></nm-button>
      <nm-button type="primary" plain text="主要按钮"></nm-button>
      <nm-button type="success" plain text="成功按钮"></nm-button>
      <nm-button type="info" plain text="信息按钮"></nm-button>
      <nm-button type="warning" plain text="警告按钮"></nm-button>
      <nm-button type="danger" plain text="危险按钮"></nm-button>
    </el-row>

    <el-row>
      <nm-button round text="圆角按钮"></nm-button>
      <nm-button type="primary" round text="主要按钮"></nm-button>
      <nm-button type="success" round text="成功按钮"></nm-button>
      <nm-button type="info" round text="信息按钮"></nm-button>
      <nm-button type="warning" round text="警告按钮"></nm-button>
      <nm-button type="danger" round text="危险按钮"></nm-button>
    </el-row>

    <el-row>
      <nm-button icon="search" circle></nm-button>
      <nm-button type="primary" icon="edit" circle></nm-button>
      <nm-button type="success" icon="select" circle></nm-button>
      <nm-button type="info" icon="permission" circle></nm-button>
      <nm-button type="warning" icon="refresh" circle></nm-button>
      <nm-button type="danger" icon="delete" circle></nm-button>
    </el-row>
    <el-row>
      <nm-button type="text" text="文字按钮"></nm-button>
      <nm-button type="text" text="文字按钮" disabled></nm-button>
    </el-row>
    <el-row>
      <nm-button type="primary" icon="edit"></nm-button>
      <nm-button type="primary" icon="search" text="搜索"></nm-button>
      <nm-button type="primary">
        刷新
        <nm-icon name="refresh"></nm-icon>
      </nm-button>
    </el-row>
    <el-row>
      <el-button-group>
        <nm-button type="primary" icon="arrow-left" text="上一页"></nm-button>
        <nm-button type="primary">
          下一页
          <nm-icon name="arrow-right"></nm-icon>
        </nm-button>
      </el-button-group>
      <el-button-group>
        <nm-button type="primary" icon="edit"></nm-button>
        <nm-button type="primary" icon="preview"></nm-button>
        <nm-button type="primary" icon="delete"></nm-button>
      </el-button-group>
    </el-row>
    <el-row>
      <nm-button type="primary" :loading="true" text="加载中"></nm-button>
    </el-row>
    <el-row>
      <el-alert title="按钮尺寸" type="warning" description="按钮默认尺寸根据系统的字号大小设置" show-icon :closable="false"></el-alert>
      <nm-button text="默认按钮"></nm-button>
      <nm-button text="中等按钮" size="medium"></nm-button>
      <nm-button text="小型按钮" size="small"></nm-button>
      <nm-button text="超小按钮" size="mini"></nm-button>
    </el-row>
  </nm-box>
</template>
<style>
</style>
