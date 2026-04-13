import tkinter as tk
from tkinter import ttk, messagebox, filedialog
from elements.image_file import ImageFile
from elements.document_file import DocumentFile
from structure.object_structure import ObjectStructure
from converters.file_converter import FileConverter

class FileConverterGUI:
    def __init__(self, root):
        self.root = root
        self.root.title("Конвертер файлов")
        self.root.geometry("900x700")
        
        self.structure = ObjectStructure()
        self.converter = FileConverter()
        self.output_path = tk.StringVar(value="C:/Users/juullsv/Desktop/ОТКРОЙ МНЯ")        
        self._create_widgets()
        self._update_file_list()
    
    def _create_widgets(self):
        top_frame = ttk.Frame(self.root, padding="10")
        top_frame.pack(fill=tk.X)
        
        ttk.Label(top_frame, text="Путь сохранения:", font=("Arial", 10)).pack(side=tk.LEFT)
        ttk.Entry(top_frame, textvariable=self.output_path, width=50).pack(side=tk.LEFT, padx=10)
        ttk.Button(top_frame, text="Обзор", command=self._browse_output).pack(side=tk.LEFT)
        
        add_frame = ttk.LabelFrame(self.root, text="Добавить файлы", padding="15")
        add_frame.pack(fill=tk.X, padx=10, pady=10)
        
        ttk.Button(add_frame, text="Добавить изображение", 
                   command=self._add_image,
                   style="Accent.TButton").pack(side=tk.LEFT, padx=10, pady=5)
        
        ttk.Button(add_frame, text="Добавить документ", 
                   command=self._add_document,
                   style="Accent.TButton").pack(side=tk.LEFT, padx=10, pady=5)
        
        ttk.Button(add_frame, text="Очистить все", 
                   command=self._clear_all,
                   style="Danger.TButton").pack(side=tk.LEFT, padx=10, pady=5)
        
        list_frame = ttk.LabelFrame(self.root, text="Файлы для конвертации", padding="10")
        list_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)
        
        self.file_list = ttk.Treeview(list_frame, columns=("name", "type", "size"), 
                                       show="headings", height=10)
        self.file_list.heading("name", text="Имя файла")
        self.file_list.heading("type", text="Тип")
        self.file_list.heading("size", text="Размер (байт)")
        self.file_list.column("name", width=500)
        self.file_list.column("type", width=150)
        self.file_list.column("size", width=150)
        self.file_list.pack(fill=tk.BOTH, expand=True)
        
        convert_frame = ttk.LabelFrame(self.root, text="Конвертация", padding="15")
        convert_frame.pack(fill=tk.X, padx=10, pady=10)
        
        ttk.Button(convert_frame, text="В JPEG", 
                   command=lambda: self._convert("jpeg")).pack(side=tk.LEFT, padx=15, pady=5)
        ttk.Button(convert_frame, text="В PNG", 
                   command=lambda: self._convert("png")).pack(side=tk.LEFT, padx=15, pady=5)
        ttk.Button(convert_frame, text="В PDF", 
                   command=lambda: self._convert("pdf")).pack(side=tk.LEFT, padx=15, pady=5)
        
        log_frame = ttk.LabelFrame(self.root, text="Журнал операций", padding="10")
        log_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)
        
        self.log_text = tk.Text(log_frame, height=10, state=tk.DISABLED, 
                                font=("Consolas", 10), bg="white")
        self.log_text.pack(fill=tk.BOTH, expand=True)
        
        scrollbar = ttk.Scrollbar(self.log_text, command=self.log_text.yview)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        self.log_text.config(yscrollcommand=scrollbar.set)
        
        self._configure_styles()
    
    def _configure_styles(self):
        style = ttk.Style()
        style.configure("Accent.TButton", 
                       font=("Arial", 11, "bold"),
                       padding=15)
        style.map("Accent.TButton",
                 background=[("active", "#4CAF50")])
        
        style.configure("Danger.TButton", 
                       font=("Arial", 11, "bold"),
                       padding=15,
                       foreground="white")
        style.map("Danger.TButton",
                 background=[("active", "#d32f2f")])
        
        style.configure("TLabelframe.Label", font=("Arial", 10, "bold"))
    
    def _browse_output(self):
        path = filedialog.askdirectory()
        if path:
            self.output_path.set(path + "/")
    
    def _add_image(self):
        path = filedialog.askopenfilename(filetypes=[("Images", "*.png *.jpg *.jpeg *.gif *.bmp")])
        if path:
            image = ImageFile(path, width=1920, height=1080, format="PNG")
            self.structure.add_file(image)
            self._update_file_list()
            self._log(f"Добавлено изображение: {path}")
    
    def _add_document(self):
        path = filedialog.askopenfilename(filetypes=[("Documents", "*.txt *.docx *.rtf *.pdf")])
        if path:
            doc = DocumentFile(path, pages=10, word_count=5000)
            self.structure.add_file(doc)
            self._update_file_list()
            self._log(f"Добавлен документ: {path}")
    
    def _clear_all(self):
        self.structure.clear()
        self._update_file_list()
        self._log("Все файлы удалены")
    
    def _update_file_list(self):
        for item in self.file_list.get_children():
            self.file_list.delete(item)
        
        for file in self.structure.get_files():
            file_type = "Изображение" if isinstance(file, ImageFile) else "Документ"
            self.file_list.insert("", tk.END, values=(file.path, file_type, file.get_size()))
    
    def _convert(self, format: str):
        if not self.structure.get_files():
            messagebox.showwarning("Внимание", "Добавьте файлы для конвертации!")
            return
        
        output = self.output_path.get()
        
        self._log(f"Запуск конвертации в {format.upper()}")
        
        for file in self.structure.get_files():
            if format == "jpeg":
                self.converter.convert_to_jpeg(file, output, quality=90)
            elif format == "png":
                self.converter.convert_to_png(file, output, compression=6)
            elif format == "pdf":
                self.converter.convert_to_pdf(file, output, page_size="A4")
        
        self._log(f"Конвертация завершена!")
        messagebox.showinfo("Готово", f"Файлы конвертированы в {format.upper()}!")
    
    def _log(self, message: str):
        self.log_text.config(state=tk.NORMAL)
        self.log_text.insert(tk.END, message + "\n")
        self.log_text.see(tk.END)
        self.log_text.config(state=tk.DISABLED)

def main():
    root = tk.Tk()
    
    style = ttk.Style()
    style.theme_use('clam')
    
    app = FileConverterGUI(root)
    root.mainloop()

if __name__ == "__main__":
    main()