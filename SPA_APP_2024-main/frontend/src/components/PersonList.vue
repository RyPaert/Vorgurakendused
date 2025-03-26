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
            <DataTable :value="persons">
                <Column field="id" header="ID" />
                <Column field="name" header="Name" />
                <Column field="city" header="Linn" />
                <Column field="region" header="Regioon" />
                <Column field="date" header="Kuupäev" />
                <Column header="Actions">
                    <template #body="{ data }">
                        <button @click="deletePerson(data.id)">Kustuta</button>
                        <button @click="editPerson(data)">Edit</button>
                    </template>
                </Column>
            </DataTable>
        </div>

        <div v-if="editingPerson" class="edit-form">
            <h2>Muuda isikut</h2>
            <form @submit.prevent="updatePerson">
                <input v-model="editingPerson.name" placeholder="Nimi" required />
                <input v-model="editingPerson.city" placeholder="Linn" required />
                <input v-model="editingPerson.region" placeholder="Regioon" required />
                <input v-model="editingPerson.date" type="date" required />
                <button type="submit">Salvesta</button>
                <button type="button" @click="editingPerson = null">Tühista</button>
            </form>
        </div>
    </div>
</template>

<script setup lang="ts">
    import { ref } from 'vue';
    import DataTable from 'primevue/datatable';
    import Column from 'primevue/column';

    interface Person {
        id: number;
        name: string;
        city: string;
        region: string;
        date: string;
    }

    const persons = ref<Person[]>([
        
    ]);

    const newPerson = ref<Omit<Person, 'id'>>({
        name: "",
        city: "",
        region: "",
        date: ""
    });

    const editingPerson = ref<Person | null>(null);

    const addPerson = () => {
        persons.value.push({
            id: persons.value.length + 1,
            ...newPerson.value
        });
        newPerson.value = { name: "", city: "", region: "", date: "" };
    };

    const deletePerson = (id: number) => {
        persons.value = persons.value.filter(p => p.id !== id);
    };

    const editPerson = (person: Person) => {
        editingPerson.value = { ...person };
    };

    const updatePerson = () => {
        if (!editingPerson.value) return;
        const index = persons.value.findIndex(p => p.id === editingPerson.value?.id);
        if (index !== -1) {
            persons.value[index] = { ...editingPerson.value };
        }
        editingPerson.value = null;
    };

</script>