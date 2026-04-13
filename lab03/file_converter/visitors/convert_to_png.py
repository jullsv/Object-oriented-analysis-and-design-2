from visitors.file_visitor import FileVisitor
from elements.image_file import ImageFile
from elements.document_file import DocumentFile
import os

class ConvertToPNGVisitor(FileVisitor):
    def __init__(self, output_path: str, compression: int = 6):
        super().__init__(output_path)
        self._compression = compression
    
    def visit_image_file(self, file: ImageFile) -> None:
        print(f"Конвертация изображения {file.path} в PNG")
        print(f"   Параметры: {file.width}x{file.height}, формат {file.format}")
        print(f"   Компрессия: {self._compression}")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.png")
        
        print(f"   Сохранение в: {output_file}")
        
        try:
            from PIL import Image
            img = Image.open(file.path)
            img.save(output_file, 'PNG', compress_level=self._compression)
            print(f"   Изображение конвертировано в PNG")
        except ImportError:
            print("   Установите Pillow: pip install Pillow")
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"PNG конвертация: {file.path}\n")
                f.write(f"Компрессия: {self._compression}\n")
            print(f"   Создан файл: {output_file}")
        except Exception as e:
            print(f"   Ошибка: {e}")
    
    def visit_document_file(self, file: DocumentFile) -> None:
        print(f"Конвертация документа {file.path} в PNG")
        print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
        print(f"   Компрессия: {self._compression}")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.png")
        
        print(f"   Сохранение в: {output_file}")
        
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(f"Документ конвертирован в PNG\n")
            f.write(f"Исходный файл: {file.path}\n")
            f.write(f"Страниц: {file.pages}\n")
            f.write(f"Слов: {file.word_count}\n")
            f.write(f"Компрессия: {self._compression}\n")
        
        print(f"   Создан файл: {output_file}")