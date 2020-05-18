import tkinter as tk
HEIGHT=300
WIDTH=400

root=tk.Tk()

canvas = tk.Canvas(root, height=HEIGHT, width=WIDTH)
canvas.pack()

background_label = tk.Label(root)
background_label.place(relwidth=1, relheight=1 )

frame=tk.Frame(root, bg='#80c1ff', bd=5)
frame.place(relx=0.5, rely=0.1, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='#80c1ff', bd=5)
frame.place(relx=0.5, rely=0.25, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='#80c1ff', bd=5)
frame.place(relx=0.5, rely=0.40, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='#80c1ff', bd=5)
frame.place(relx=0.5, rely=0.55, relwidth=0.75, relheight=0.1, anchor='n')

frame=tk.Frame(root, bg='#80c1ff', bd=5)
frame.place(relx=0.5, rely=0.70, relwidth=0.75, relheight=0.1, anchor='n')

root.mainloop()