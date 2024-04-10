import { useEffect, useState } from "react";
import Note from "./Note";
import useAuthAxios from "../../hooks/useAuthAxios";

const Notes = () => {
    const [notes, setNotes] = useState([]);
    const [newTitle, setNewTitle] = useState('');
    const [newDescription, setNewDescription] = useState('');
    const [error, setError] = useState('');

    const axios = useAuthAxios();

    useEffect(() => {
        const fetchNotes = async () => {
            const response = await axios.get("/notes");
            setNotes(response.data);
        };

        fetchNotes();
    }, [axios]);

    const handleAddUser = async () => {
        if (newTitle.trim() === '' || newDescription.trim() === '') {
        return;
        }

        const newNote = { title: newTitle, description: newDescription };
        const response = await axios
            .post("/notes", newNote)
            .catch(error => setError(error));

        setNotes([...notes, { id: response.data, ...newNote }]);
        setNewTitle('');
        setNewDescription('');
    };

    const handleRemoveNote = async (id) => {
        await axios.delete(`/notes/${id}`)
            .catch(error => setError(error));
        const updatedNotes = notes.filter(user => user.id !== id);
        setNotes(updatedNotes);
    };

    return (
        <div className="container mx-auto mt-8">
        <h1 className="text-2xl font-bold mb-4">Admin Page</h1>
        <div className="mb-4">
            <input
            type="text"
            className="border border-gray-400 px-4 py-2 mr-2"
            placeholder="Enter title"
            value={newTitle}
            onChange={(e) => setNewTitle(e.target.value)}
            />
            <input
            type="text"
            className="border border-gray-400 px-4 py-2 mr-2"
            placeholder="Enter description"
            value={newDescription}
            onChange={(e) => setNewDescription(e.target.value)}
            />
            <button
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            onClick={handleAddUser}
            >
            Add Note
            </button>
        </div>
        {error.length !== 0 && <p className='text-red-500'>{error}</p>}
        <div>
            {notes.length > 0 ? (
            <ul>
                {notes.map(note => (
                <Note
                    key={note.id} 
                    note={note} 
                    onRemove={handleRemoveNote} 
                />
                ))}
            </ul>
            ) : (
            <p>No notes found.</p>
            )}
        </div>
        </div>
    );
}

export default Notes;