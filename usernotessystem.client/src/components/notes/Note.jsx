const Note = ({ note, onRemove }) => (
    <li className="flex items-center justify-between mb-2">
      <span>{note.title} - {note.description}</span>
      <button
        className="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600"
        onClick={() => onRemove(note.id)}
      >
        Remove
      </button>
    </li>
  );

  export default Note;