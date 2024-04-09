import React, { useState, useEffect } from 'react';

interface Patient
{
    id?: number;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    address: string;
}

const PatientsList: React.FC = () => {
    const [patients, setPatients] = useState<Patient[]>([]);
    const [newPatientFirstName, setNewPatientFirstName] = useState<string>('');
    const [newPatientLastName, setNewPatientLastName] = useState<string>('');
    const [newPatientDateOfBirth, setNewPatientDateOfBirth] = useState<string>('');
    const [newPatientAddress, setNewPatientAddress] = useState<string>('');

    useEffect(() => {
        fetchPatients();
    }, []);

    const fetchPatients = async () => {
        try {
            const response = await fetch('/api/patients');
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            setPatients(data);
        } catch (error) {
            console.error('There was a problem with your fetch operation:', error);
        }
    };

    const handleNewPatientSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        const newPatient = {
            firstName: newPatientFirstName,
            lastName: newPatientLastName,
            dateOfBirth: newPatientDateOfBirth,
            address: newPatientAddress,
        };
        await createPatient(newPatient, fetchPatients);
        setNewPatientFirstName('');
        setNewPatientLastName('');
        setNewPatientDateOfBirth('');
        setNewPatientAddress('');
    }

    return (
        <div>
            <h2>Patients</h2>
            <form onSubmit={handleNewPatientSubmit}>
                <input
                    type="text"
                    placeholder="First Name"
                    value={newPatientFirstName}
                    onChange={(e) => setNewPatientFirstName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Last Name"
                    value={newPatientLastName}
                    onChange={(e) => setNewPatientLastName(e.target.value)}
                />
                <input
                    type="date"
                    placeholder="Date of Birth"
                    value={newPatientDateOfBirth}
                    onChange={(e) => setNewPatientDateOfBirth(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Address"
                    value={newPatientAddress}
                    onChange={(e) => setNewPatientAddress(e.target.value)}
                />
                <button type="submit">Add Patient</button>
            </form>
            <ul>
                {patients.map(patient => (
                    <li key={patient.id}>{patient.firstName} {patient.lastName}</li>
                ))}
            </ul>
        </div>
    );
};

export default PatientsList;

type FetchPatientsFunction = () => Promise<void>;

const createPatient = async (newPatient: Patient, fetchPatients: FetchPatientsFunction) => {
    try {
        const response = await fetch('/api/patients', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newPatient),
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        await fetchPatients();
        // Handle the response
    } catch (error) {
        console.error('There was a problem with your fetch operation:', error);
    }
};