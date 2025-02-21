<template>
  <div class="about">
    <h1>Personlist</h1>
    <div class="personlist">
      <DataTable :value="persons" v-if="persons.length > 0 ">
        <Column field="id" header="Person id" style="color: white; "/>
        <Column field="name" header="Name" style="color: white; "/>
        <Column field="city" header="Linn" style="color: white; "/>
        <Column field="region" header="Asukoht" style="color: white;"/>
        <Column field="date" header="Kuupäev" style="color: white;"/>
      </DataTable>
      <div v-else>Isikud puuduvad</div>
    </div>
  </div>
</template>


<script setup lang="ts">
import { type Person } from '@/models/person';
import { usePersonsStore } from "@/stores/personsStore";
import { storeToRefs } from "pinia";
import { defineProps, onMounted, watch, ref  } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

watch(route, (to, from) => {
  if (to.path !== from.path || to.query !== from.query) {
    personsStore.load();
  }
}, { deep: true });

defineProps<{ title: String }>();
    const personsStore = usePersonsStore();
    const { persons } = storeToRefs(personsStore);

onMounted(async () => {
    personsStore.load();
});
</script>
<style>

</style>