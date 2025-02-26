<template>
    <div class="about">
        <h1>Personlist</h1>

        <form @submit.prevent="addPerson">
            <input v-model="newPerson.name" placeholder="Nimi" required />
            <input v-model="newPerson.city" placeholder="Linn" required />
            <input v-model="newPerson.region" placeholder="Regioon" required />
            <input v-model="newPerson.date" type="date" required />
            <button type="submit">Lisa inimene</button>
        </form>

        <div class="personlist">
            <DataTable :value="persons" v-if="persons.length > 0">
                <Column field="id" header="Person id" />
                <Column field="name" header="Name" />
                <Column field="city" header="Linn" />
                <Column field="region" header="Asukoht" />
                <Column field="date" header="Kuupäev" />
                <Column header="Tegevused">
                    <template #body="{ data }">
                        <button @click="deletePerson(data.id)">Kustuta</button>
                        <button @click="editPerson(data)">Muuda</button>
                    </template>
                </Column>
            </DataTable>
            <div v-else>Isikud puuduvad</div>
        </div>

        <div v-if="editingPerson" class="edit-form">
            <h2>Muuda isikut</h2>
            <form @submit.prevent="updatePerson">
                <input v-model="editingPerson.name" placeholder="Nimi" required />
                <input v-model="editingPerson.city" placeholder="Linn" required />
                <input v-model="editingPerson.region" placeholder="Regioon" required />
                <input v-model="editingPerson.date" type="date" required />
                <button type="submit">Salvesta muudatused</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
    import { usePersonsStore } from "@/stores/personsStore";
    import { storeToRefs } from "pinia";
    import { ref, watch, onMounted } from "vue";
    import { useRoute } from "vue-router";

    const route = useRoute();
    const personsStore = usePersonsStore();
    const { persons } = storeToRefs(personsStore);

    const newPerson = ref({
        name: "",
        city: "",
        region: "",
        date: ""
    });

    const editingPerson = ref<Person | null>(null);

    onMounted(() => {
        personsStore.load();
    });

    const addPerson = async () => {
        if (!newPerson.value.name || !newPerson.value.city || !newPerson.value.region || !newPerson.value.date) {
            alert("fill all fields");
            return;
        }
        await personsStore.addPerson({ ...newPerson.value });
        newPerson.value = { name: "", city: "", region: "", date: "" };
    };

    const deletePerson = async (id: number) => {
        await personsStore.deletePerson(id);
    };

    const editPerson = (person: Person) => {
        editingPerson.value = { ...person };
    };

    const updatePerson = async () => {
        if (!editingPerson.value) return;
        await personsStore.updatePerson(editingPerson.value);
        editingPerson.value = null;
    };
</script>

<style scoped>
</style>
