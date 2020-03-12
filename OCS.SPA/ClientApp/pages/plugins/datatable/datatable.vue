<template>
        <datatable v-bind="data" />
</template>

<script>

export default {
  props: {
    data: {
      type: Object
    }
  }
};
</script>
