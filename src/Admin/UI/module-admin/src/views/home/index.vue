<template>
  <nm-container scrollbar>
    <nm-box-row height="400px">
      <nm-box-col :span="12">
        <top-left />
      </nm-box-col>
      <nm-box-col :span="12">
        <top-right />
      </nm-box-col>
    </nm-box-row>
    <nm-box-row height="500px">
      <nm-box-col :span="24">
        <bottom />
      </nm-box-col>
    </nm-box-row>
  </nm-container>
</template>
<script>
import page from './page'
import TopLeft from './components/top-left'
import TopRight from './components/top-right'
import Bottom from './components/bottom'
export default {
  name: page.name,
  components: { TopLeft, TopRight, Bottom }
}
</script>
