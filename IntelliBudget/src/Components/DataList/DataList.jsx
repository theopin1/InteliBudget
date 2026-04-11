import { useState } from 'react';
import './DataList.css';

const DataList = ({
  title = 'Lista',
  groups = [],
  formFields = [],
  onAdd,
  onEdit,
  onDelete,
  textColor,
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const [formData, setFormData] = useState({});
  const [editingId, setEditingId] = useState(null);

  const handleChange = (name, value) => {
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = () => {
    if (editingId !== null) {
      if (onEdit) onEdit(editingId, formData);
    } else {
      if (onAdd) onAdd(formData);
    }
    setFormData({});
    setEditingId(null);
    setIsOpen(false);
  };

  const handleClose = () => {
    setFormData({});
    setEditingId(null);
    setIsOpen(false);
  };

  const handleOpenEdit = (group) => {
    const prefilled = {};
    formFields.forEach((field, index) => {
      prefilled[field.name] = group.fields[index]?.value ?? '';
    });
    setFormData(prefilled);
    setEditingId(group.id);
    setIsOpen(true);
  };

  return (
    <div className="datalist-card" style={textColor ? { color: textColor } : {}}>

      <div className="datalist-header">
        <h2 className="datalist-title">{title}</h2>
        {formFields.length > 0 && (
          <button className="datalist-toggle-btn" onClick={() => setIsOpen(true)}>
            + Adicionar
          </button>
        )}
      </div>

      {groups.length === 0 ? (
        <p className="datalist-empty">Nenhum dado encontrado.</p>
      ) : (
        <ul className="datalist-list">
          {groups.map((group, groupIndex) => (
            <li key={groupIndex} className="datalist-group">
              {group.fields.map((field, index) => (
                <div key={index} className="datalist-item">
                  <span className="datalist-label">{field.label}</span>
                  <span className="datalist-value">{field.value}</span>
                </div>
              ))}
              <div className="datalist-actions">
                {onEdit && (
                  <button className="datalist-edit-btn" onClick={() => handleOpenEdit(group)}>
                    Editar
                  </button>
                )}
                {onDelete && (
                  <button className="datalist-delete-btn" onClick={() => onDelete(group.id)}>
                    Excluir
                  </button>
                )}
              </div>
              {groupIndex < groups.length - 1 && (
                <hr className="datalist-divider" />
              )}
            </li>
          ))}
        </ul>
      )}

      {isOpen && (
        <div className="datalist-overlay" onClick={handleClose}>
          <div className="datalist-modal" onClick={(e) => e.stopPropagation()}>
            <div className="datalist-modal-header">
              <h3 className="datalist-modal-title">
                {editingId !== null ? `Editar ${title}` : `Adicionar ${title}`}
              </h3>
              <button className="datalist-close-btn" onClick={handleClose}>✕</button>
            </div>

            <div className="datalist-form">
              {formFields.map((field) => (
                <div key={field.name} className="datalist-form-field">
                  <label className="datalist-form-label">{field.label}</label>
                  <input
                    type={field.type || 'text'}
                    value={formData[field.name] || ''}
                    onChange={(e) => handleChange(field.name, e.target.value)}
                    className="datalist-form-input"
                    placeholder={field.placeholder || ''}
                  />
                </div>
              ))}
            </div>

            <div className="datalist-modal-footer">
              <button className="datalist-cancel-btn" onClick={handleClose}>Cancelar</button>
              <button className="datalist-submit-btn" onClick={handleSubmit}>Salvar</button>
            </div>
          </div>
        </div>
      )}

    </div>
  );
};

export default DataList;